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
    class TooSmallItemException : RefidrigatorExceptions
    {
        public TooSmallItemException() { }
    }
    class NoSuchRefigrigatorException : RefidrigatorExceptions
    {
        public NoSuchRefigrigatorException() { }
    }
    class FridgeAlreadyExistsException : RefidrigatorExceptions
    {
        public FridgeAlreadyExistsException() { }
    }
    class BigItemCoolingException : RefidrigatorExceptions
    {
        public BigItemCoolingException() { }
    }
    class ShelfAlreadyRemovedException : RefidrigatorExceptions
    {
        public ShelfAlreadyRemovedException() { }
    }
    class NoCompatibleShelfException : RefidrigatorExceptions
    {
        public NoCompatibleShelfException() { }
    }


}
