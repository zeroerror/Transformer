using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using FixMath.NET;
using GameArki.TripodCamera;
using GameArki.FreeInput;
using Transformer.Bussiness.LogicBussiness;
using Transformer.Bussiness.RendererBussiness;
using Transformer.Bussiness.UIBussiness;
using Transformer.Bussiness.LogicBussiness.Facade;

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

            rendererCore.Tick();
            uiCore.Tick();
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
            setter.Bind(1, KeyCode.W);
            setter.Bind(2, KeyCode.S);
            setter.Bind(3, KeyCode.A);
            setter.Bind(4, KeyCode.D);

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
                var setterAPI = camCore.SetterAPI;
                var roleTF = GameObject.Find("role").transform;
                setterAPI.Follow_SetInit_Current(roleTF, new Vector3(0, 10, -10), GameArki.FPEasing.EasingType.Linear, 0.02f);
                setterAPI.LookAt_SetInit_Current(roleTF, new Vector3(0, 0, 0));

                var rb = roleTF.GetComponent<Rigidbody>();
                logicCore.logicFacade.Domain.RoleDomain.SpawnRole(1000, Bussiness.LogicBussiness.Generic.ControlType.Owner, rb);
                sceneLoaded = true;
            };
        }

        void Inject()
        {
            logicCore.Inject(inputCore, allTemplate);
            rendererCore.Inject(camCore);
        }

    }

}

