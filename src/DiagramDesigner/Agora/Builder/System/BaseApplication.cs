using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agora.Builder.System {
    public class BaseApplication {
        public static BaseApplication MainInstance { get; set; }
        public int CurrentPluginID { get; set; }
        public object []Memory { get; set; }
        public object GetMyMemory() {
            return Memory[CurrentPluginID];   
        }
        public BaseApplication(int pluginCount) {
            Memory = new object[pluginCount];
        }
        static BaseApplication() {
            BaseApplication.MainInstance = new BaseApplication(100);
        }
    }
}
