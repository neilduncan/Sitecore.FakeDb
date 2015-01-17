﻿namespace Sitecore.FakeDb.Tests.Data.Fields
{
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Fields;
  using Xunit;

  /// <summary>
  /// Internal link: <link text="Link to Home item" linktype="internal" class="default" title="Home" target='Active Browser' querystring="sc_lang=en" id="{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}" />
  /// External link: <link text="Gmail" linktype="external" url="http://gmail.com" anchor="" title="Google mail" class="link" target="Active Browser" />
  /// </summary>
  public class LinkFieldTest
  {
    private const string FieldName = "Link";

    [Fact]
    public void ShouldSetLinkFieldPropertiesUsingRawValue()
    {
      // arrange
      using (var db = new Db
                        {
                          new DbItem("home")
                            {
                              { FieldName, "<link linktype=\"external\" url=\"http://google.com\" />" }
                            }
                        })
      {
        var item = db.GetItem("/sitecore/content/home");

        // act
        var linkField = (LinkField)item.Fields[FieldName];

        // assert
        linkField.LinkType.Should().Be("external");
        linkField.Url.Should().Be("http://google.com");
      }
    }

    [Fact]
    public void ShouldSetLinkFieldPropertiesUsingDbLinkField()
    {
      // arrange
      var targetId = ID.NewID;

      using (var db = new Db
                        {
                          new DbItem("home")
                            {
                              new DbLinkField(FieldName)
                                {
                                  LinkType = "internal", 
                                  QueryString = "sc_lang=en",
                                  TargetID = targetId
                                }
                            }
                        })
      {
        var item = db.GetItem("/sitecore/content/home");

        // act
        var linkField = (LinkField)item.Fields[FieldName];

        // assert
        linkField.LinkType.Should().Be("internal");
        linkField.QueryString.Should().Be("sc_lang=en");
        linkField.TargetID.Should().Be(targetId);
      }
    }
  }
}