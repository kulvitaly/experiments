﻿using LeetCode.Collections;

namespace LeetCode.Tests.Collections;

public class StackSolutionTests
{
    [Theory]
    [InlineData("()", true)]
    //[InlineData("()[]{}", true)]
    //[InlineData("(]", false)]
    //[InlineData("([)]", false)]
    //[InlineData("{[]}", true)]
    public void IsValid_InputData_ShouldSucceed(string value, bool expected)
    {
        Assert.Equal(expected, new StackSolution().IsValid(value));
    }
}
