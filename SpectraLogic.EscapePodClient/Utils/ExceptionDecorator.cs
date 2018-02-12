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

using SpectraLogic.EscapePodClient.Exceptions;
using SpectraLogic.EscapePodClient.Model;
using System;
using System.Net;

namespace SpectraLogic.EscapePodClient.Utils
{
    internal class ExceptionDecorator
    {
        #region Methods

        public static T Run<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (ErrorResponseException ex)
            {
                if (ex.ErrorResponse.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new InvalidEscapePodServerCredentialsException(ex.ErrorResponse.ErrorMessage, ex);
                }

                if (ex.ErrorResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    var notFoundErrorResponse = ex.ErrorResponse as NotFoundErrorResponse;

                    switch (notFoundErrorResponse.ResourceType)
                    {
                        case ResourceType.Archive:
                            throw new ArchiveNotFoundException(ex.ErrorResponse.ErrorMessage, ex);
                        case ResourceType.JOB:
                            throw new ArchiveJobNotFoundException(ex.ErrorResponse.ErrorMessage, ex);
                        case ResourceType.BUCKET:
                            throw new BucketDoesNotExistException(ex.ErrorResponse.ErrorMessage, ex);
                    }
                }

                if (ex.ErrorResponse.StatusCode == HttpStatusCode.Conflict)
                {
                    //TODO need to wait for Conflict error response to be implemented in the server
                }

                if (ex.ErrorResponse.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ClusterNotConfiguredException(ex.ErrorResponse.ErrorMessage, ex);
                }

                throw ex;
            }
        }

        #endregion Methods
    }
}