﻿namespace Sitecore.FakeDb.Tests
{
  using System.Linq;
  using FluentAssertions;
  using Sitecore.Data;
  using Xunit;

  public class DbTemplateTest
  {
    [Fact]
    public void ShouldCreateEmptyFieldsCollection()
    {
      // arrange
      var template = new DbTemplate();

      // act & assert
      template.Fields.Should().BeEmpty();
    }

    [Fact]
    public void ShouldCreateEmptyFieldsCollectionWhenSetNameAndId()
    {
      // arrange
      var template = new DbTemplate("t", ID.NewID);

      // act & assert
      template.Fields.Should().BeEmpty();
    }

    // TODO:[High] The test below states that we cannot get fake item fields by id.
    [Fact]
    public void ShouldCreateTemplateFieldsUsingNamesAsLowercaseKeys()
    {
      // arrange
      var template = new DbTemplate { "Title", "Description" };

      // assert
      template.Fields.Select(f => f.Name).ShouldBeEquivalentTo(new[] { "Title", "Description" });
    }
  }
}