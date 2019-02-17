using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace GithubStatistics.Common.Enums
{
    public static class LimitColor
    {
        public static Color GetColorByIndex(int index)
        {
            Color c;
            switch (index)
            {
                case 0:
                    c = Colors.Red;
                    break;
                case 1:
                    c = Colors.OrangeRed;
                    break;
                case 2:
                    c = Colors.Green;
                    break;
                case 3:
                    c = Colors.GreenYellow;
                    break;
                case 4:
                    c = Colors.White;
                    break;
                default:
                    c = Colors.White;
                    break;
            }
            return c;
        }
    }
}
