using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
   class StaticBitmap 
    {
       public Bitmap bitmap;

        public StaticBitmap(int X, int Y)
        {
            bitmap = new Bitmap(X,Y);
        }

    }
}
