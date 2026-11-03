using Xunit;
using Todo.Core;
using System.Linq;

namespace Todo.Core.Tests
{
    public class TodoListTests
    {
        [Fact]
        public void Add_IncreasesCount()
        {
            var list = new TodoList();
            list.Add("  task  ");
            Assert.Equal(1, list.Count);
            Assert.Equal("task", list.Items.First().Title);
        }

        [Fact]
        public void Remove_ById_Works()
        {
            var list = new TodoList();
            var item = list.Add("a");
            Assert.True(list.Remove(item.Id));
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Find_ReturnsMatches()
        {
            var list = new TodoList();
            list.Add("Buy milk");
            list.Add("Read book");
            var found = list.Find("buy").ToList();
            Assert.Single(found);
            Assert.Equal("Buy milk", found[0].Title);
        }

        [Fact]
        public void MarkDone_SetsFlag()
        {
            var list = new TodoList();
            var item = list.Add("test");
            item.MarkDone();
            Assert.True(item.IsDone);
        }

        [Fact]
        public void MarkUndone_ClearsFlag()
        {
            var list = new TodoList();
            var item = list.Add("test");
            item.MarkDone();
            item.MarkUndone();
            Assert.False(item.IsDone);
        }

        [Fact]
        public void Rename_ChangesTitle()
        {
            var list = new TodoList();
            var item = list.Add("old");
            item.Rename("new");
            Assert.Equal("new", item.Title);
        }

        [Fact]
        public void Save_And_Load_PreservesData()
        {
            var tempFile = Path.GetTempFileName();
            try
            {
                var list = new TodoList();
                list.Add("Task 1");
                var item2 = list.Add("Task 2");
                item2.MarkDone();
                list.Save(tempFile);

                var loaded = new TodoList();
                loaded.Load(tempFile);

                Assert.Equal(2, loaded.Count);
                Assert.Equal("Task 1", loaded.Items[0].Title);
                Assert.Equal("Task 2", loaded.Items[1].Title);
                Assert.False(loaded.Items[0].IsDone);
                Assert.True(loaded.Items[1].IsDone);
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [Fact]
        public void Load_ThrowsIfFileNotFound()
        {
            var list = new TodoList();
            Assert.Throws<FileNotFoundException>(() => list.Load("nonexistent.json"));
        }
    }
}
