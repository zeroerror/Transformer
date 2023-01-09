using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using FixMath.NET;
using GameArki.TripodCamera;
using GameArki.FreeInput;
using ZeroPhysics.Extensions;
using Transformer.Bussiness.LogicBussiness;
using Transformer.Bussiness.LogicBussiness.Generic;
using Transformer.Bussiness.RendererBussiness;
using Transformer.UIBussiness;
using Transformer.Generic;
using Transformer.Template;
using Transformer.Bussiness.RendererAPI;

namespace Transformer.Entry
{

    public class MainEntry : MonoBehaviour
    {

        // === Core
        TCCore camCore;
        FreeInputCore inputCore;

        LogicCore logicCore;
        RendererCore rendererCore;
        UICore uiCore;

        // === Temlate
        AllTemplate allTemplate;

        bool sceneLoaded = false;

        void Awake()
        {
            DontDestroyOnLoad(this);
            Action action = async () =>
            {
                await Init();
                Inject();
            };
            action.Invoke();
        }

        void Update()
        {
            if (!sceneLoaded) return;

            var dt = UnityEngine.Time.deltaTime;

            // Logic
            logicCore.Tick(dt);

            // Renderer
            logicCore.Update(dt);
            rendererCore.Update(dt);
            uiCore.Update(dt);
        }

        void LateUpdate()
        {
            if (!sceneLoaded) return;

            camCore.Tick(UnityEngine.Time.deltaTime);
        }

        async Task Init()
        {
            // - Core
            camCore = new TCCore();
            camCore.Initialize(Camera.main);
            camCore.SetterAPI.SpawnByMain(0);

            inputCore = new FreeInputCore();
            var setter = inputCore.Setter;
            InputKeyCodeModel inputKeyCodeModel = new InputKeyCodeModel();
            inputKeyCodeModel.DefaultInit();
            setter.Bind(InputBindIDCollection.JUMP, inputKeyCodeModel.jump_key);
            setter.Bind(InputBindIDCollection.MOVE_FORWARD, inputKeyCodeModel.moveForward_key);
            setter.Bind(InputBindIDCollection.MOVE_BACKWARD, inputKeyCodeModel.moveBackward_key);
            setter.Bind(InputBindIDCollection.MOVE_LEFT, inputKeyCodeModel.moveLeft_key);
            setter.Bind(InputBindIDCollection.MOVE_RIGHT, inputKeyCodeModel.moveRight_key);

            logicCore = new LogicCore();

            rendererCore = new RendererCore();

            uiCore = new UICore();

            // - Asset
            allTemplate = new AllTemplate();
            await allTemplate.Init();

            // - Scene
            var handle = Addressables.LoadSceneAsync("scene_field_1000", LoadSceneMode.Single);
            handle.Completed += (op) =>
            {
                var fieldSO = allTemplate.FieldTempate.TryGet(1000);
                var physicsCore = logicCore.PhysicsCore;

                // - Logic
                FPVector3 bornFPPos = fieldSO.ToFPBornPos();
                var logicRole = logicCore.logicFacade.Domain.RoleDomain.SpawnRole(1000, ControlType.Owner, bornFPPos);

                var tfModels = fieldSO.transformModels;
                for (int i = 0; i < tfModels.Length; i++)
                {
                    var tfModel = tfModels[i];
                    var api = physicsCore.SetterAPI;
                    api.SpawnBox(tfModel.ToFPCenter(), tfModel.ToFPQuaternion(), tfModel.ToFPScale(), tfModel.ToFPSize());
                }

                // - Renderer
                Vector3 bornPos = fieldSO.ToBornPos();
                rendererCore.RendererFacade.Domain.RoleDomain.SpawnRole(1000, logicRole.IDComponent.ID, bornPos);

                var rootTF = handle.Result.Scene.GetRootGameObjects()[0].transform;
                var roleTF = new GameObject("Role").transform;
                roleTF.position = bornPos;
                roleTF.SetParent(rootTF, true);

                var cameraSetterAPI = camCore.SetterAPI;
                cameraSetterAPI.Follow_SetInit_Current(roleTF, new Vector3(0, 10, -10), GameArki.FPEasing.EasingType.Linear, 0f);
                cameraSetterAPI.LookAt_SetInit_Current(roleTF, new Vector3(0, 0, 0));

                sceneLoaded = true;
            };
        }

        void Inject()
        {
            rendererCore.Inject(camCore, allTemplate);

            RendererSetter rendererSetter = new RendererSetter();
            rendererSetter.Inject(rendererCore.RendererFacade);
            logicCore.Inject(inputCore, allTemplate, rendererSetter);
        }

        void OnDrawGizmos()
        {
            if (logicCore == null) return;

            var physicsCore = logicCore.PhysicsCore;
            var getterAPI = physicsCore.GetterAPI;
            var allBoxes = getterAPI.GetAllBoxes();
            var allRBBoxes = getterAPI.GetAllRBBoxes();

            allBoxes.ForEach((box) =>
            {
                box.DrawBoxBorder();
                box.DrawBoxPoint();
            });
            allRBBoxes.ForEach((rbBox) =>
            {
                var box = rbBox.Box;
                box.DrawBoxBorder();
                box.DrawBoxPoint();
            });
        }

    }

}

