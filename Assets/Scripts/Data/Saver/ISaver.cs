using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameApplication.Data.Saver {
    public interface ISaver {
        void Save();

        void Load();
    }
}
