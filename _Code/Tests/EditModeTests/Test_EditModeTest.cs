using VamVam.Scripts.Utils;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace VamVam.EditModeTests {

    public class Test_EditModeTest {
        
        private ILog _logSubstitute;
        private Calculator _calculator;

        [SetUp]
        public void SetUp() {
            // This function executes before each test
            _logSubstitute = Substitute.For<ILog>();
            _calculator = new Calculator(_logSubstitute);
        }

        [TearDown]
        public void TearDown() {
            // This function executes after each test
        }


        [Test]
        public void Sum_ParametersArePositive_ReturnsCorrectResult() {
            var result = _calculator.Sum(10, 20);

            Assert.AreEqual(30, result, "El resultado de la suma es incorrecto");
        }

        [TestCase(-1,  1)]
        [TestCase( 1, -1)]
        public void Sum_NegativeParameters_ThrowsException(int value1, int value2) {
            // Asegurarnos que se lanza una excepción cuando se da un número negativo a Sum()
            Assert.Throws<System.Exception>(() => {
                var result = _calculator.Sum(value1, value2);
            });
        }

        [TestCase(10, 20, 30)]
        [TestCase(2, 1, 3)]
        [TestCase(0, 0, 0)]
        public void Sum_ParametersArePositive_ReturnsCorrectLog(int value1, int value2, int result) {
            // act
            _calculator.Sum(value1, value2);

            // assert // optional    v  (times called)
            // logSubstitute.Received().Log(Arg.Any<string>());                     // Comprueba que hay un string en el log
            // logSubstitute.Received().Log($"{value1} => {value2} = {result}");    // Comprueba que el log es exactamente como está escrito acá
            _logSubstitute.Received().Log(
                Arg.Is<string>(
                    s => s.Contains(result.ToString())                              // Comprueba que el resultado está presente en el log
                    &&   s.Contains(value1.ToString())                              // Comprueba que el valor 1 está presente en el log
                    &&   s.Contains(value2.ToString())                              // Comprueba que el valor 2 está presente en el log
                )
            );
            
        }

        [Test]
        public void TestCount() {
            _logSubstitute.LogCount().Returns(info => {
                // Lógica extra
                return 123;                             // Reemplaza el return de la función
            });

            Debug.Log(_logSubstitute.LogCount());
        }

    }
}
                
