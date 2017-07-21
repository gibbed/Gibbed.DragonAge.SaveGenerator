﻿/* Copyright (c) 2017 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;

namespace Gibbed.DragonAge.SaveGenerator.Game
{
    [Flags]
    public enum AwakeningOptions
    {
        None = 0,
        Orlesian = 1 << 0,
        AndersRecruited = 1 << 1,
        NathanielRecruited = 1 << 2,
        NathanielFriendly = 1 << 3,
        AmaranthineSaved = 1 << 4,
        HerrenCompletedSilverite = 1 << 5,
        AndersDiedInSiege = 1 << 6,
        NathanielDiedInSiege = 1 << 7,
        VigilsKeepSiegeCompleted = 1 << 8,
    }
}