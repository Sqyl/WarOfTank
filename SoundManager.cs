using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.PropertyGridInternal;
using WarOfTank.Properties;

namespace WarOfTank
{
    internal class SoundManager
    {
        private static SoundPlayer startPlayer = new SoundPlayer();
        private static SoundPlayer addPlayer = new SoundPlayer();
        private static SoundPlayer blastPlayer = new SoundPlayer();
        private static SoundPlayer firePlayer = new SoundPlayer();
        private static SoundPlayer hitPlayer = new SoundPlayer();
        public static void InitSound()
        {
            startPlayer.Stream = Resources.start;
            addPlayer.Stream = Resources.add;
            blastPlayer.Stream = Resources.blast;
            firePlayer.Stream = Resources.fire;
            hitPlayer.Stream = Resources.hit;
        }
        public static void GameStart()
        {
            startPlayer.Play();
        }

        public static void GameAdd() {
            addPlayer.Play();
        }

        public static void GameBlast()
        {
            blastPlayer.Play();
        }

        public static void GameFire()
        {
            firePlayer.Play();
        }

        public static void GameHit() {
            hitPlayer.Play();
        }
    }
}
