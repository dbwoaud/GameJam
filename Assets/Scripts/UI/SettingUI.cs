public class SettingUI : BaseUI
{
    public void OnChangeBGMVolume(float value)
    {
        SoundManager.Instance.ChangeVoulme(SoundType.BGM,value);
    }

    public void OnChangeSFXVolume(float value)
    {
        SoundManager.Instance.ChangeVoulme(SoundType.SFX,value);
    }
}
