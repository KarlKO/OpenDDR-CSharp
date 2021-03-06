﻿/**
 * Copyright 2011 OpenDDR LLC
 * This software is distributed under the terms of the GNU Lesser General Public License.
 *
 *
 * This file is part of OpenDDR Simple APIs.
 * OpenDDR Simple APIs is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, version 3 of the License.
 *
 * OpenDDR Simple APIs is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with Simple APIs.  If not, see <http://www.gnu.org/licenses/>.
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oddr.Models.Vocabularies
{
    public class VocabularyVariable
    {
        public String aspect
        {
            get; set;
        }
        public String id
        {
            get; set;
        }
        public String name
        {
            get; set;
        }
        public String vocabulary
        {
            get; set;
        }

        public override string ToString()
        {
            return "VocabularyVariable {" + " aspect=" + this.aspect + " id=" + this.id + " name=" + this.name + " vocabulary=" + this.vocabulary + '}';
        }
    }
}
