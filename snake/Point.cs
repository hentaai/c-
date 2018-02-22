using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace snake
{
    [Serializable, XmlRoot("Point")]
    public class Point
    {
        public int x,y;
        public Point(){}        
        public Point(int _x,int _y){
            x = _x;
            y = _y;
        }
    }
}