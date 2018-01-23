﻿/*
 * ******************************************************************************
 *   Copyright 2014-2018 Spectra Logic Corporation. All Rights Reserved.
 *   Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *   this file except in compliance with the License. A copy of the License is located at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 *   or in the "license" file accompanying this file.
 *   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *   CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *   specific language governing permissions and limitations under the License.
 * ****************************************************************************
 */

using Newtonsoft.Json;

namespace SpectraLogic.EscapePodClient.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class RestoreFile
    {
        [JsonProperty(Order = 1, PropertyName = "name")] private string Name;
        [JsonProperty(Order = 2, PropertyName = "destination")] private string Destination;
        [JsonProperty(Order = 3, PropertyName = "restoreFileAttributes", NullValueHandling = NullValueHandling.Ignore)] private bool? RestoreFileAttributes;
        [JsonProperty(Order = 4, PropertyName = "byteRange", NullValueHandling = NullValueHandling.Ignore)] private ByteRange ByteRange;
        [JsonProperty(Order = 5, PropertyName = "timeCodeRange", NullValueHandling = NullValueHandling.Ignore)] private TimecodeRange TimeCodeRange;

        [JsonConstructor]
        private RestoreFile() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestoreFile"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="restoreFileAttributes">if set to <c>true</c> [restore file attributes].</param>
        public RestoreFile(string name, string destination, bool restoreFileAttributes)
        {
            Name = name;
            Destination = destination;
            RestoreFileAttributes = restoreFileAttributes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestoreFile"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="byteRange">The byte range.</param>
        public RestoreFile(string name, string destination, ByteRange byteRange)
        {
            Name = name;
            Destination = destination;
            ByteRange = byteRange;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestoreFile"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="timeCodeRange">The time code range.</param>
        public RestoreFile(string name, string destination, TimecodeRange timeCodeRange)
        {
            Name = name;
            Destination = destination;
            TimeCodeRange = timeCodeRange;
        }
    }
}
