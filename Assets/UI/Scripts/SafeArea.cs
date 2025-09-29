using UnityEngine;
using Device = UnityEngine.Device;

[ExecuteInEditMode, RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    private RectTransform rectTransform;

    private DrivenRectTransformTracker tracker = new();

    private ScreenOrientation orientation;

#if UNITY_EDITOR
    private string deviceModel;

    private void EditorCacheDevice() => deviceModel = Device.SystemInfo.deviceModel;
    private bool EditorDeviceChanged => Device.SystemInfo.deviceModel != deviceModel;
#else
    private void EditorCacheDevice() { }
    private const bool EditorDeviceChanged = false;
#endif

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        orientation = Device.Screen.orientation;
        EditorCacheDevice();
    }

    private void Update()
    {
        if (Device.Screen.orientation != orientation || EditorDeviceChanged)
        {
            orientation = Device.Screen.orientation;
            EditorCacheDevice();
            SetProperties();
        }
    }

    private void OnEnable()
    {
        tracker.Clear();
        tracker.Add(this, rectTransform, DrivenTransformProperties.All);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += SetProperties;
#else
        SetProperties();
#endif
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall -= SetProperties;
#endif

        tracker.Clear();
    }

    private void SetProperties()
    {
        rectTransform.pivot = new(0.5f, 0.5f);
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector3.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        var screenSize = new Vector2(Device.Screen.width, Device.Screen.height);
        rectTransform.anchorMin = Device.Screen.safeArea.min / screenSize;
        rectTransform.anchorMax = Device.Screen.safeArea.max / screenSize;
    }
}
