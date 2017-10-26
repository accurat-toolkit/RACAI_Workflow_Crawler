using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agora.Builder.System;

namespace Agora.Builder.Interfaces {
    public interface DecisionBlock {
        /// <summary>
        /// Called upon condition validation. After this the GetDataMethod is called to extract the dataflow object
        /// </summary>
        /// <param name="data">Passed by the previous block - contains all or part of the dataflow</param>
        /// <returns>False or True</returns>
        bool EvaluateCondition(object data, BaseApplication MyApplication);
        /// <summary>
        /// Called for data extraction. If there is no processing to be done just "return data;"
        /// </summary>
        /// <param name="data">Dataflow object</param>
        /// <returns>Returns processed or non-processed dataflow object</returns>
        object GetData(object data, BaseApplication MyApplication);
    }
}
