using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using FixMath.NET;
using GameArki.TripodCamera;
using GameArki.FreeInput;
using ZeroPhysics.Extensions;
using Transformer.LogicBussiness;
using Transformer.LogicBussiness.Facade;
using Transformer.LogicBussiness.Generic;
using Transformer.RendererBussiness;
using Transformer.UIBussiness;
using Transformer.Generic;

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
            Init();
            Inject();
        }

        void Update()
        {
            if (!sceneLoaded) return;

            logicCore.Update();
            rendererCore.Update();
            uiCore.Update();
        }

        void FixedUpdate()
        {
            if (!sceneLoaded) return;
            logicCore.Tick(FP64.ToFP64(UnityEngine.Time.fixedDeltaTime));
        }

        void LateUpdate()
        {
            if (!sceneLoaded) return;

            camCore.Tick(UnityEngine.Time.deltaTime);
        }

        async void Init()
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
            var handle = Addressables.LoadSceneAsync("scene_game_level1", LoadSceneMode.Single);
            handle.Completed += (op) =>
            {
                var roleTF = GameObject.Find("role").transform;
                var cameraSetterAPI = camCore.SetterAPI;
                cameraSetterAPI.Follow_SetInit_Current(roleTF, new Vector3(0, 10, -10), GameArki.FPEasing.EasingType.Linear, 0f);
                cameraSetterAPI.LookAt_SetInit_Current(roleTF, new Vector3(0, 0, 0));

                logicCore.logicFacade.Domain.RoleDomain.SpawnRole(1000, ControlType.Owner, FPVector3.Zero);
                sceneLoaded = true;
            };
        }

        void Inject()
        {
            logicCore.Inject(inputCore, allTemplate);
            rendererCore.Inject(camCore);
        }

        void OnDrawGizmos()
        {
            if (logicCore == null) return;

            var roleRepo = logicCore.logicFacade.Repo.RoleRepo;
            roleRepo.ForeachAll((role) =>
            {
                var lc = role.LocomotionComponent;
                lc.RbBox.Box.DrawBoxBorder();
            });
        }

    }

}

