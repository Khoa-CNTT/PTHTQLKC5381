using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHOM20_DATN.res.service.library
{
    public class AutoGenerateID
    {
        public string GenerateRandomIdWordStart(string head)
        {
            string prefix = head;
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var randomStr = new string(Enumerable.Repeat(chars, 7)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return prefix + randomStr;
        }
    }
}