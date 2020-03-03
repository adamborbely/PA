using System;

namespace com.codecool.api
{
    public class RefidrigatorExceptions : Exception
    {
        public RefidrigatorExceptions() { }
    }
    public class CannotCoolItException : RefidrigatorExceptions
    {
        public CannotCoolItException() { }
    }
    public class NoEmptyShelfPlaceException : RefidrigatorExceptions
    {
        public NoEmptyShelfPlaceException() { }
    }
    public class SizeNotCompatableException : RefidrigatorExceptions
    {
        public SizeNotCompatableException() { }
    }
    public class FridgeIsClosedException : RefidrigatorExceptions
    {
        public FridgeIsClosedException() { }
    }
    public class FridgeIsFullException : RefidrigatorExceptions
    {
        public FridgeIsFullException() { }
    }
    public class FoodNotExistsException : RefidrigatorExceptions
    {
        public FoodNotExistsException() { }
    }
    public class TooSmallItemException : RefidrigatorExceptions
    {
        public TooSmallItemException() { }
    }
    public class NoSuchRefigrigatorException : RefidrigatorExceptions
    {
        public NoSuchRefigrigatorException() { }
    }
    public class FridgeAlreadyExistsException : RefidrigatorExceptions
    {
        public FridgeAlreadyExistsException() { }
    }
    public class BigItemCoolingException : RefidrigatorExceptions
    {
        public BigItemCoolingException() { }
    }
    public class ShelfAlreadyRemovedException : RefidrigatorExceptions
    {
        public ShelfAlreadyRemovedException() { }
    }
    public class NoCompatibleShelfException : RefidrigatorExceptions
    {
        public NoCompatibleShelfException() { }
    }
    public class NotEnoughShelfException : RefidrigatorExceptions
    {
        public NotEnoughShelfException() { }
    }
    public class NoShelfInTheFridgeException : RefidrigatorExceptions
    {
        public NoShelfInTheFridgeException() { }
    }

}
