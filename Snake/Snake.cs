// ----------------------------------------------------------------------- 
// <copyright file="Snake.cs" company="FHWN"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This program works with geometric objects.</summary> 
// <author>Wolfgang Ofner</author> 
// -----------------------------------------------------------------------
namespace Snake
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for Snake.
    /// </summary>
    internal class Snake
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Snake"/> class.
        /// </summary>
        /// <param name="col">Col coordinate.</param>
        /// <param name="row">Row coordinate.</param>
        internal Snake(int col, int row)
        {
            this.Row = row;
            this.Col = col;
        }

        /// <summary>
        /// Gets or sets the Row coordinate.
        /// </summary>
        internal int Row { get; set; }

        /// <summary>
        /// Gets or sets the Col coordinate.
        /// </summary>
        internal int Col { get; set; }
    }
}
