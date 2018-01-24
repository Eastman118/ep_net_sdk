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
    public class EscapePodJobStatus
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        [JsonProperty(Order = 1, PropertyName = "message")] public string Message { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(Order = 2, PropertyName = "status")] public JobStatus Status { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EscapePodJobStatus"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="status">The status.</param>
        public EscapePodJobStatus(string message, JobStatus status)
        {
            Message = message;
            Status = status;
        }

        //TODO add a ToString function for pretty printing
    }

    /// <summary>
    /// 
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// The active
        /// </summary>
        ACTIVE,

        /// <summary>
        /// The in progress
        /// </summary>
        IN_PROGRESS,

        /// <summary>
        /// The canceled
        /// </summary>
        CANCELED,

        /// <summary>
        /// The done
        /// </summary>
        DONE,
    }
}