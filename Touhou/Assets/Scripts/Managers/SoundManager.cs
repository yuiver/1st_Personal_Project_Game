using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    // DontDestroyOnLoad로 로드되는 사운드매니저에 아무생각없이 Dictionary에 지역을 이동할때마다 캐싱하게 된다면 메모리 누수가 생기기 때문에 Clear함수를 넣어서 메모리를 관리한다.
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    //Mp3 Player    ->AudioSource
    //Mp3 음원      ->AudioClip
    //관객 (귀)     ->AudioListener
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop= true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }


    //랩핑해서 오디오 클립을 받는 버전
    public void Play (string path, Define.Sound type = Define.Sound.Effect , float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    //DB에서 받을때는 경로로 받지만 그전까지 귀찮으니 오디오 클립으로 받는다.
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {


        if (type == Define.Sound.Bgm)
        {
            if (audioClip == null)
            {
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path , Define.Sound type = Define.Sound.Effect)
    {
        //사운드 경로가 생략되었다면 사운드 경로를 Resources/Sounds로 생성해준다!
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }
        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing ! {path}");
        }

        return audioClip;
    }
}
