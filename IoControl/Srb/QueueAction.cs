using System;
using System.Collections.Generic;
using System.Text;

namespace IoControl.Srb
{
    public static class QueueAction
    {
        /// <summary>
        /// SRB_SIMPLE_TAG_REQUEST
        /// </summary>
        public const byte SimpleTagRequest = 0x20;
        /// <summary>
        /// SRB_HEAD_OF_QUEUE_TAG_REQUEST
        /// </summary>
        public const byte HeadOfQueueTagRequest = 0x21;
        /// <summary>
        /// SRB_ORDERED_QUEUE_TAG_REQUEST
        /// </summary>
        public const byte OrderedQueueTagRequest = 0x22;
        /// <summary>
        /// SRB_WMI_FLAGS_ADAPTER_REQUEST
        /// </summary>
        public const byte WmiFlagsAdapterRequest = 0x01;
        /// <summary>
        /// SRB_POWER_FLAGS_ADAPTER_REQUEST
        /// </summary>
        public const byte PowerFlagsAdapterRequest = 0x01;
        /// <summary>
        /// SRB_PNP_FLAGS_ADAPTER_REQUEST
        /// </summary>
        public const byte PnpFlagsAdapterRequest = 0x01;
    }
}
