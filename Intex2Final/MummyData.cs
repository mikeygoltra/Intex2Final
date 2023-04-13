using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeaddirectionModelTest
{
    public class MummyData
    {
        [Required]
        public float SexMale { get; set; }
        [Required]
        public float SexFemale { get; set; }
        [Required]
        public float Wrapping_S { get; set; }
        [Required]
        public float SamplesCollected { get; set; }
        [Required]
        public float Wrapping_B { get; set; }
        [Required]
        public float AgeAtDeath_C { get; set; }
        [Required]
        public float SampleCollected_F { get; set; }
        [Required]
        public float Wrapping_W { get; set; }
        [Required]
        public float Area_SE { get; set; }
        [Required]
        public float Wrapping_H { get; set; }


        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
            SexMale, SexFemale, Wrapping_S, SamplesCollected, Wrapping_B,
                AgeAtDeath_C, SampleCollected_F, Wrapping_W, Area_SE, Wrapping_H
            };
            int[] dimensions = new int[] { 1, 10 };

            var validationContext = new ValidationContext(this);
            var validationResults = new List<ValidationResult>();

            // Validate the instance of the class
            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                // If validation fails, throw an exception with a custom error message
                throw new Exception("Please fill in all required fields.");
            }

            return new DenseTensor<float>(data, dimensions);
        }
    }
}
