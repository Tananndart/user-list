namespace UserListTests
{
    public interface IGenericTestCase
    {
        void Construct_WithCollection_ShouldEqualsItems();

        void Construct_WithEnumerable_ShouldValid();

        void Construct_WithCapacity_ShouldValid();

        void SetCapacity_ShouldSaveItems();

        void Add_ShouldAddItem();

        void Clear_ShouldRemoveAllItems();

        void Remove_ByIndex_ShouldRemoveItem();

        void Remove_ByItem_ShouldRemoveItem();

        void Insert_ShouldInsertItem();
    }
}
