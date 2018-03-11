using Explore.Fixie.Tests.Framework;
using FluentAssertions;

namespace Explore.Fixie.Tests.Scenarios
{
    public class CalculatorScenarios
    { 
        [Example(2, 3, 5)]
        [Example(0, 0, 0)]
        [Example(-1, 3, 2)]
        public void given_two_operands_when_adding_then_should_return_sum(int operand1, int operand2, int expected)
        {
            var calculator = new Calculator();
            calculator.Add(operand1, operand2).Should().Be(expected);
        }

        public void GIVEN_two_operands_WHEN_subtracting_THEN_should_return_the_result()
        {
            var calculator = new Calculator();
            calculator.Subtract(5, 3).Should().Be(2);
        }

        [Example(2, 1, 2)]
        [Example(-2, 5, -10)]
        public void GivenTwoOperandsWhenMultiplyingThenSHouldReturnTheFactorial(int operand1, int operand2, int expected)
        {
            var calculator = new Calculator();
            calculator.Multiply(operand1, operand2).Should().Be(expected);
        }
    }
}
