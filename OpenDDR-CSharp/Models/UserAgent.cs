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
using System.Text.RegularExpressions;

namespace Oddr.Models
{
    public class UserAgent
    {

        public const String MOZILLA_AND_OPERA_PATTERN = "(.*?)((?:Mozilla)|(?:Opera))[/ ](\\d+\\.\\d+).*?\\(((?:.*?)(?:.*?\\(.*?\\))*(?:.*?))\\)(.*)";
        public const int INDEX_MOZILLA_PATTERN_GROUP_PRE = 1;
        public const int INDEX_MOZILLA_PATTERN_GROUP_INSIDE = 4;
        public const int INDEX_MOZILLA_PATTERN_GROUP_POST = 5;
        public const int INDEX_MOZILLA_PATTERN_GROUP_MOZ_VER = 3;
        public const int INDEX_OPERA_OR_MOZILLA = 2;
        private readonly Regex mozillaPatternCompiled = null;
        private readonly Regex versionPatternCompiled = null;
        public String completeUserAgent
        {
            private set;
            get;
        }

        public bool mozillaPattern
        {
            private set;
            get;
        }

        public bool operaPattern
        {
            private set;
            get;
        }

        public String mozillaVersion
        {
            private set;
            get;
        }

        public String operaVersion
        {
            private set;
            get;
        }

        public bool containsAndroid
        {
            private set;
            get;
        }

        public bool containsBlackBerryOrRim
        {
            private set;
            get;
        }

        public bool containsIOSDevices
        {
            private set;
            get;
        }

        public bool containsMSIE
        {
            private set;
            get;
        }

        public bool containsSymbian
        {
            private set;
            get;
        }

        public bool containsWindowsPhone
        {
            private set;
            get;
        }

        private String[] patternElements;

        internal UserAgent(String userAgent)
        {
            if (userAgent == null)
            {
                throw new ArgumentNullException("userAgent can not be null");
            }
            completeUserAgent = userAgent;

            Match result = null;
            try
            {
                result = mozillaPatternCompiled.Match(userAgent);

            }
            catch (Exception ex)
            {
                mozillaPatternCompiled = new Regex(MOZILLA_AND_OPERA_PATTERN);
                result = mozillaPatternCompiled.Match(userAgent);
            }

            if (result.Success)
            {
                patternElements = new String[]{
                        result.Groups[INDEX_MOZILLA_PATTERN_GROUP_PRE].Value,
                        result.Groups[INDEX_MOZILLA_PATTERN_GROUP_INSIDE].Value,
                        result.Groups[INDEX_MOZILLA_PATTERN_GROUP_POST].Value
                    };
                String version = result.Groups[INDEX_MOZILLA_PATTERN_GROUP_MOZ_VER].Value;
                if (result.Groups[INDEX_OPERA_OR_MOZILLA].Value.Contains("Opera"))
                {
                    mozillaPattern = false;
                    operaPattern = true;
                    operaVersion = version;

                    if (operaVersion.Equals("9.80") && patternElements[2] != null)
                    {
                        Match result2 = null;

                        try
                        {
                            result2 = versionPatternCompiled.Match(patternElements[2]);

                        }
                        catch (Exception ex)
                        {
                            versionPatternCompiled = new Regex(".*Version/(\\d+.\\d+).*");
                            result2 = versionPatternCompiled.Match(patternElements[2]);
                        }

                        if (result2.Success)
                        {
                            operaVersion = result2.Groups[1].Value;
                        }
                    }

                }
                else
                {
                    mozillaPattern = true;
                    mozillaVersion = version;
                }

            }
            else
            {
                mozillaPattern = false;
                operaPattern = false;
                patternElements = new String[]{
                        null,
                        null,
                        null};
                mozillaVersion = null;
                operaVersion = null;
            }

            if (userAgent.Contains("Android"))
            {
                containsAndroid = true;

            }
            else
            {
                containsAndroid = false;
                Regex iPadRegex = new Regex(".*(?!like).iPad.*");
                Regex iPodRegex = new Regex(".*(?!like).iPod.*");
                Regex iPhoneRegex = new Regex(".*(?!like).iPhone.*");

                if (iPadRegex.IsMatch(userAgent) || iPodRegex.IsMatch(userAgent) || iPhoneRegex.IsMatch(userAgent))
                {
                    containsIOSDevices = true;

                }
                else
                {
                    containsIOSDevices = false;
                    Regex blackBerryRegex = new Regex(".*[Bb]lack.?[Bb]erry.*|.*RIM.?Tablet.?OS.*");
                    if (blackBerryRegex.IsMatch(userAgent))
                    {
                        containsBlackBerryOrRim = true;

                    }
                    else
                    {
                        containsBlackBerryOrRim = false;
                        Regex symbianRegex = new Regex(".*Symbian.*|.*SymbOS.*|.*Series.?60.*");
                        if (symbianRegex.IsMatch(userAgent))
                        {
                            containsSymbian = true;

                        }
                        else
                        {
                            containsSymbian = false;
                            Regex windowsRegex = new Regex(".*Windows.?(?:(?:CE)|(?:Phone)|(?:NT)|(?:Mobile)).*");
                            if (windowsRegex.IsMatch(userAgent))
                            {
                                containsWindowsPhone = true;

                            }
                            else
                            {
                                containsWindowsPhone = false;
                            }

                            Regex internetExplorerRegex = new Regex(".*MSIE.([0-9\\.b]+).*");
                            if (internetExplorerRegex.IsMatch(userAgent))
                            {
                                containsMSIE = true;
                                
                            }
                            else
                            {
                                containsMSIE = false;
                            }
                        }
                    }
                }
            }
        }

        public String GetPatternElementsPre()
        {
            return patternElements[0];
        }

        public String GetPatternElementsInside()
        {
            return patternElements[1];
        }

        public String GetPatternElementsPost()
        {
            return patternElements[2];
        }
    }
}
