﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpectraLogic.SpectraRioBrokerClient.Model
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="SpectraLogic.SpectraRioBrokerClient.Model.IRelationship" />
    public class Relationship : IRelationship
    {
        #region Public Constructors

        /// <summary>Initializes a new instance of the <see cref="Relationship"/> class.</summary>
        /// <param name="objects">The objects.</param>
        /// <param name="page">The page.</param>
        public Relationship(IList<BrokerObject> objects, PageResult page)
        {
            Objects = objects;
            Page = page;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets the objects.
        /// </summary>
        /// <value>
        /// The objects.
        /// </value>
        [JsonProperty(PropertyName = "objects")] public IList<BrokerObject> Objects { get; }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        [JsonProperty(PropertyName = "page")] public PageResult Page { get; }

        #endregion Public Properties
    }
}