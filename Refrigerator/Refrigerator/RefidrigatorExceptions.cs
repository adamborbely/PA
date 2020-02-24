using System;
using System.Collections.Generic;
using System.Text;

namespace com.codecool.api
{
    class RefidrigatorExceptions : Exception
    {
        public RefidrigatorExceptions() { }
    }
    class CannotCoolItException : RefidrigatorExceptions
    {
        public CannotCoolItException() { }
    }
    class NoEmptyShelfPlaceException : RefidrigatorExceptions
    {
        public NoEmptyShelfPlaceException() { }
    }
    class SizeNotCompatableException : RefidrigatorExceptions
    {
        public SizeNotCompatableException() { }
    }
    class FridgeIsClosedException : RefidrigatorExceptions
    {
        public FridgeIsClosedException() { }
    }
    class FridgeIsFullException : RefidrigatorExceptions
    {
        public FridgeIsFullException() { }
    }
    class FoodNotExistsException : RefidrigatorExceptions
    {
        public FoodNotExistsException() { }
    }
}
