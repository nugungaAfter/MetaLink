//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Player/PlayerInputAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputAction"",
    ""maps"": [
        {
            ""name"": ""FirstPerson"",
            ""id"": ""c1dfb33f-7edc-4472-983c-067dff1de47f"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6948bd0b-ecb0-4096-acca-6872322e5c7b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""View"",
                    ""type"": ""Value"",
                    ""id"": ""8914044c-93db-4b85-8dce-ce43a40e57d2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""c25cf0eb-5c14-4e98-b458-cac809d07b15"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""99365ddc-4905-4796-96f3-eae208589a3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""6387f5f5-f1c5-4a97-9531-999488b6a92d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Grab"",
                    ""type"": ""Button"",
                    ""id"": ""b02f66c1-5e26-4aa6-860a-30ece4557fe4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""13b9977e-0d11-4082-b6c3-7308f1e5dfe8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""b41b28ad-55ca-4fd7-99cd-f19ef90a8f49"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""1601120d-53f8-480e-9bd1-0ad5b818b3d7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""126ee81c-85f9-4ac6-a2ee-9da53323e609"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""dc06c605-b470-4f0a-b4f4-ddb826bd0d4a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""c71adf6b-1c78-418c-b15a-eb694f5d43d8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4aa932e3-9770-4d68-aeee-3dd11f087119"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""View"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3fc5249-2b54-41dc-9e36-b262d5363747"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12967707-bee0-4265-bb22-5316e64292b6"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d92be2f8-ccc6-4949-a0d5-1d30df6603a5"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b57d5ba-0d36-4113-aaad-5bb9127870dc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46be2ece-f484-4d22-a93f-09f3a91c8b53"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // FirstPerson
        m_FirstPerson = asset.FindActionMap("FirstPerson", throwIfNotFound: true);
        m_FirstPerson_Move = m_FirstPerson.FindAction("Move", throwIfNotFound: true);
        m_FirstPerson_View = m_FirstPerson.FindAction("View", throwIfNotFound: true);
        m_FirstPerson_Jump = m_FirstPerson.FindAction("Jump", throwIfNotFound: true);
        m_FirstPerson_Run = m_FirstPerson.FindAction("Run", throwIfNotFound: true);
        m_FirstPerson_Crouch = m_FirstPerson.FindAction("Crouch", throwIfNotFound: true);
        m_FirstPerson_Grab = m_FirstPerson.FindAction("Grab", throwIfNotFound: true);
        m_FirstPerson_Interaction = m_FirstPerson.FindAction("Interaction", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // FirstPerson
    private readonly InputActionMap m_FirstPerson;
    private IFirstPersonActions m_FirstPersonActionsCallbackInterface;
    private readonly InputAction m_FirstPerson_Move;
    private readonly InputAction m_FirstPerson_View;
    private readonly InputAction m_FirstPerson_Jump;
    private readonly InputAction m_FirstPerson_Run;
    private readonly InputAction m_FirstPerson_Crouch;
    private readonly InputAction m_FirstPerson_Grab;
    private readonly InputAction m_FirstPerson_Interaction;
    public struct FirstPersonActions
    {
        private @PlayerInputAction m_Wrapper;
        public FirstPersonActions(@PlayerInputAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_FirstPerson_Move;
        public InputAction @View => m_Wrapper.m_FirstPerson_View;
        public InputAction @Jump => m_Wrapper.m_FirstPerson_Jump;
        public InputAction @Run => m_Wrapper.m_FirstPerson_Run;
        public InputAction @Crouch => m_Wrapper.m_FirstPerson_Crouch;
        public InputAction @Grab => m_Wrapper.m_FirstPerson_Grab;
        public InputAction @Interaction => m_Wrapper.m_FirstPerson_Interaction;
        public InputActionMap Get() { return m_Wrapper.m_FirstPerson; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FirstPersonActions set) { return set.Get(); }
        public void SetCallbacks(IFirstPersonActions instance)
        {
            if (m_Wrapper.m_FirstPersonActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnMove;
                @View.started -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnView;
                @View.performed -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnView;
                @View.canceled -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnView;
                @Jump.started -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnJump;
                @Run.started -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnRun;
                @Crouch.started -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnCrouch;
                @Grab.started -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnGrab;
                @Grab.performed -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnGrab;
                @Grab.canceled -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnGrab;
                @Interaction.started -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_FirstPersonActionsCallbackInterface.OnInteraction;
            }
            m_Wrapper.m_FirstPersonActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @View.started += instance.OnView;
                @View.performed += instance.OnView;
                @View.canceled += instance.OnView;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Grab.started += instance.OnGrab;
                @Grab.performed += instance.OnGrab;
                @Grab.canceled += instance.OnGrab;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
            }
        }
    }
    public FirstPersonActions @FirstPerson => new FirstPersonActions(this);
    public interface IFirstPersonActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnView(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnGrab(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
    }
}
