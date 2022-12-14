<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="yes" />

  <xsl:template match="/">

    <xsl:for-each select="Configuration/Directories/Directory">

        <xsl:variable name="normalizeClassDesc" select="normalize-space(Desc)" />
        <xsl:if test="$normalizeClassDesc != '' and $normalizeClassDesc != ' '">
        // <xsl:value-of select="$normalizeClassDesc"/>
        </xsl:if>
        class <xsl:value-of select="Name"/>
        {
            <xsl:for-each select="Fields/Field">
                <xsl:variable name="normalizeFieldDesc" select="normalize-space(Desc)" />
                <xsl:if test="$normalizeFieldDesc != '' and $normalizeFieldDesc != ' '">
                /* <xsl:value-of select="$normalizeFieldDesc"/> */
                </xsl:if>
                <xsl:call-template name="FieldType" />
                <xsl:text> </xsl:text>
                <xsl:value-of select="Name"/> { get; set; } = <xsl:call-template name="DefaultFieldValue" />;
            </xsl:for-each>
        }
        
    </xsl:for-each>

  </xsl:template>

  <xsl:template name="FieldType">
    <xsl:choose>
      <xsl:when test="Type = 'string'">
        <xsl:text>string</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'integer'">
        <xsl:text>int</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'numeric'">
        <xsl:text>decimal</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'boolean'">
        <xsl:text>bool</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'time'">
        <xsl:text>TimeSpan</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'date' or Type = 'datetime'">
        <xsl:text>DateTime</xsl:text>
      </xsl:when>
	  <xsl:when test="Type = 'bytea'">
	    <xsl:text>byte[]</xsl:text>
      </xsl:when>
    </xsl:choose>    
  </xsl:template>

  <xsl:template name="DefaultFieldValue">
    <xsl:choose>
      <xsl:when test="Type = 'string'">
        <xsl:text>""</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'integer'">
        <xsl:text>0</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'numeric'">
        <xsl:text>0</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'boolean'">
        <xsl:text>false</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'time'">
        <xsl:text>DateTime.MinValue.TimeOfDay</xsl:text>
      </xsl:when>
      <xsl:when test="Type = 'date' or Type = 'datetime'">
        <xsl:text>DateTime.MinValue</xsl:text>
      </xsl:when>
	  <xsl:when test="Type = 'bytea'">
	    <xsl:text>new byte[] { }</xsl:text>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>