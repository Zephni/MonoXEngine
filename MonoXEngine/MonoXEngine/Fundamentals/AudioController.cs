using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoXEngine
{
    public class AudioController
    {
        private string Path;
        private ContentManager Content;
        private Dictionary<string, SoundEffectInstance> SoundEffects;

        private float masterVolume;
        public float MasterVolume
        {
            get {return masterVolume;}
            set {masterVolume = (value < 0) ? 0 : (value > 1) ? 1 : value;}
        }

        public AudioController(string path, ContentManager content)
        {
            Path = path;
            Content = content;
            SoundEffects = new Dictionary<string, SoundEffectInstance>();
        }

        public SoundEffectInstance Play(string filename)
        {
            if(!SoundEffects.ContainsKey(filename))
                SoundEffects.Add(filename, Content.Load<SoundEffect>(Path + "/" + filename).CreateInstance());

            SoundEffects[filename].Play();

            return SoundEffects[filename];
        }

        public void Pause(string filename)
        {
            SoundEffects[filename].Pause();
        }

        public void Stop(string filename)
        {
            SoundEffects[filename].Stop();
        }

        public void SetVolume(string filename, float value)
        {
            SoundEffects[filename].Volume = value;
        }

        public float GetVolume(string filename)
        {
            return SoundEffects[filename].Volume;
        }
    }
}
