/**
 * @author Darron Schall
 * @version 1.0
 * 
 * Aug. 24, 2003
 * 
 * PasswordGenerator for creating random passwords.  
 * 
 * Four password flags are available to dictate generation,
 * or a template can be specificed to generate a string with 
 * a specified character type at each position.
 * 
 * Revision History:
 * Rev Date			Who		Description
 * 1.0 8/24/03		darron	Initial Draft
 * --------------------------------------
 * License For Use
 * --------------------------------------
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice, this
 * list of conditions and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 * this list of conditions and the following disclaimer in the documentation
 * and/or other materials provided with the distribution.
 * 
 * 3. The name of the author may not be used to endorse or promote products derived
 * from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR "AS IS" AND ANY EXPRESS OR IMPLIED
 * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
 * EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
 * EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT
 * OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
 * IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY
 * OF SUCH DAMAGE.
 */

using System;

namespace ETradeCommon
{
    /// <summary>
    /// Summary description for PasswordGenerator.
    /// </summary>
    public class PasswordGenerator
    {
        public const int MIN_PASSWORD_LENGTH = 3;

        // The C# random class is a little easier to use
        // than Java's Math.random, as you'll see in the 
        // random methods later on...
        private static readonly Random Random = new Random();

        /**
     * @return a random lowercase character from 'a' to 'z'
     */
        private static char RandomLowercase()
        {
            return (char)Random.Next('a', 'z' + 1);
        }

        /**
	 * @return a random uppercase character from 'A' to 'Z'
	 */
        private static char RandomUppercase()
        {
            return (char)Random.Next('A', 'Z' + 1);
        }

        /**
	 * @return a random character in this list: !"#$%&'()*+,-./
	 */
        private static char RandomOther()
        {
            return (char)Random.Next('!', '/' + 1);
        }


        /**
	 * @return a random character from '0' to '9'
	 */
        private static char RandomNumber()
        {
            return (char)Random.Next('0', '9' + 1);
        }

        // C# lets us use "delegates" to create a variable
        // that stores a reference to a function...
        delegate char RandomCharacter();

        public static string GeneratePassword(int length, bool isLowercaseIncluded, bool isNumbersIncluded, bool isUppercaseIncluded)
        {
            bool isOthersIncluded = false;

            string password = "";

            RandomCharacter[] r = new RandomCharacter[4];

            // keep track of how many array locations we're actually using
            int count = 0;

            if (isLowercaseIncluded)
            {
                // using our delegate, store a reference to the randomLowercase
                // function in our array
                r[count++] = new RandomCharacter(PasswordGenerator.RandomLowercase);
            }
            if (isUppercaseIncluded)
            {
                r[count++] = new RandomCharacter(PasswordGenerator.RandomUppercase);
            }
            if (isOthersIncluded)
            {
                r[count++] = new RandomCharacter(PasswordGenerator.RandomOther);
            }
            if (isNumbersIncluded)
            {
                r[count++] = new RandomCharacter(PasswordGenerator.RandomNumber);
            }

            for (int i = 0; i < length; i++)
            {
                password += r[(int)Random.Next(0, count)]();
            }

            return password;
        } // end generatePassword method
    }
}