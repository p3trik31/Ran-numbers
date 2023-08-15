using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NahodnaCisla
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

      
        
        public static uint XORShift2(uint seed)
        {
            uint x = seed;
            x = x ^ (x << 13);
            x = x ^ (x >> 17);
            x = x ^ (x << 15);

            return x;
        }

        private const uint N = 624;
        private const uint M = 397;
        private const uint K = 0x9908B0DFU;

        private static uint[] state = new uint[N + 1];
        private static uint nextRand;
        private static int left = -1;

        private static uint HighestBit(uint u)
        {
            return ((u) & 0x80000000U);
        }

        private static uint LowestBit(uint u)
        {
            return ((u) & 0x00000001U);
        }

        private static uint LowestBits(uint u)
        {
            return ((u) & 0x7FFFFFFFU);
        }

        private static uint MixBits(uint u, uint v)
        {
            return (HighestBit(u) | LowestBits(v));
        }

        public static void Seed(uint seed)
        {
            uint x = (seed | 1U) & 0xFFFFFFFFU;
            uint[] s = state;
            int j;
            int i = 0;

            for (left = 0, s[i++] = x, j = (int)N; Convert.ToBoolean(--j); s[i++] = (x *= 69069U) & 0xFFFFFFFFU) ;
        }

        private static uint Znovu()
        {
            uint p0 = 0;
            uint p2 = 2;
            uint pM = M;
            uint s0;
            uint s1;
            int j;

            if (left < -1)
            {
                Seed(4357U);
            }

            left = (int)(N - 1);
            nextRand = state[1];

            for (s0 = state[0], s1 = state[1], j = (int)(N - M + 1); Convert.ToBoolean(--j); s0 = s1, s1 = state[p2++])
            {
                state[p0++] = state[pM++] ^ (MixBits(s0, s1) >> 1) ^ (Convert.ToBoolean(LowestBit(s1)) ? K : 0U);
            }

            for (pM = 0, j = (int)M; Convert.ToBoolean(--j); s0 = s1, s1 = state[p2++])
            {
                state[p0++] = state[pM++] ^ (MixBits(s0, s1) >> 1) ^ (Convert.ToBoolean(LowestBit(s1)) ? K : 0U);
            }

            s1 = state[0];
            state[p0] = state[pM] ^ (MixBits(s0, s1) >> 1) ^ (Convert.ToBoolean(LowestBit(s1)) ? K : 0U);
            s1 ^= (s1 >> 11);
            s1 ^= (s1 << 7) & 0x9D2C5680U;
            s1 ^= (s1 << 15) & 0xEFC60000U;

            return (s1 ^ (s1 >> 18));
        }


        public static uint Random()
        {
            uint y;

            if (--left < 0)
            {
                return Znovu();
            }

            y = nextRand++;
            y ^= (y >> 11);
            y ^= (y << 7) & 0x9D2C5680U;
            y ^= (y << 15) & 0xEFC60000U;

            return (y ^ (y >> 18));
        }

        private void xorshift2_Click(object sender, RoutedEventArgs e)
        {
            int opakovani = Convert.ToInt32(xor_opakovani.Text);
            uint seed = Convert.ToUInt32(seed_xor.Text);
            for(int x = 0; x < opakovani; x++)
            {
                
                textboxx.Text += XORShift2(seed) + " ";
                seed = XORShift2(seed);
               
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            int x = 0;
            uint z = Convert.ToUInt32(mid_seed.Text);
            while (x < 1)
            {
                Seed(z);
                for(int y = 0; y < Convert.ToInt32(mid_opak.Text); y++)
                {
                    uint rand = Random();
                    z = rand;
                    textboxx.Text += rand.ToString() + " ";
                }
                x++;

            }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            textboxx.Clear();
        }
    }
    }

