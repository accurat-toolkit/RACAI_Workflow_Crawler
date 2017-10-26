using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agora.Builder.System;

namespace Agora.Builder.Interfaces {
    public interface ProcessingBlock {
        /// <summary>
        /// This method is called when it must process the dataflow
        /// </summary>
        /// <param name="data">Passed by the previous block - contains all or part of the dataflow</param>
        /// <returns></returns>
        object ProcessData(object data, BaseApplication MyApplication);
    }
}
