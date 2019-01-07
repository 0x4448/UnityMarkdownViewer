﻿////////////////////////////////////////////////////////////////////////////////

using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using UnityEngine;

namespace MG.MDV
{
    /// <see cref="Markdig.Renderers.HtmlRenderer"/>

    public class RendererMarkdown : RendererBase
    {
        GUISkin mSkin;

        public RendererMarkdown( GUISkin skin )
        {
            mSkin = skin;

            ObjectRenderers.Add( new RendererBlockCode() );
            ObjectRenderers.Add( new RendererBlockList() );
            ObjectRenderers.Add( new RendererBlockHeading() );
            ObjectRenderers.Add( new RendererBlockHtml() );
            ObjectRenderers.Add( new RendererBlockParagraph() );
            ObjectRenderers.Add( new RendererBlockQuote() );
            ObjectRenderers.Add( new RendererBlockThematicBreak() );

            ObjectRenderers.Add( new RendererInlineLink() );
            ObjectRenderers.Add( new RendererInlineAutoLink() );
            ObjectRenderers.Add( new RendererInlineCode() );
            ObjectRenderers.Add( new RendererInlineDelimiter() );
            ObjectRenderers.Add( new RendererInlineEmphasis() );
            ObjectRenderers.Add( new RendererInlineLineBreak() );
            ObjectRenderers.Add( new RendererInlineHtml() );
            ObjectRenderers.Add( new RendererInlineHtmlEntity() );
            ObjectRenderers.Add( new RendererInlineLiteral() );
        }

        public override object Render( MarkdownObject document )
        {
            Write( document );
            return null;
        }


        //------------------------------------------------------------------------------

        internal GUIStyle GetStyle( string style )
        {
            return mSkin != null ? mSkin.GetStyle( style ) : null;
        }

        /// <see cref="Markdig.Renderers.TextRendererBase.WriteLeafInline"/>

        internal void WriteLeafBlockInline( LeafBlock block )
        {
            var inline = block.Inline as Inline;

            while( inline != null )
            {
                Write( inline );
                inline = inline.NextSibling;
            }
        }

        /// <see cref="Markdig.Renderers.HtmlRenderer.WriteLeafRawLines"/>

        internal void WriteLeafRawLines( LeafBlock block )
        {
            if( block.Lines.Lines == null )
            {
                return;
            }

            var lines  = block.Lines;
            var slices = lines.Lines;

            for( int i = 0; i < lines.Count; i++ )
            {
                // if( !writeEndOfLines && i > 0 )
                // {
                //     WriteLine();
                // }
                // if( escape )
                // {
                //     WriteEscape( ref slices[ i ].Slice, softEscape );
                // }
                // else
                // {
                //     Write( ref slices[ i ].Slice );
                // }
                // if( writeEndOfLines )
                // {
                //     WriteLine();
                // }
            }

            throw new System.NotImplementedException();
        }


        string mLine = string.Empty;

        internal void Print( string text )
        {
            mLine += text;
        }

        internal string GetLine()
        {
            var line = mLine;
            mLine = string.Empty;
            return line;
        }

        internal void EnsureLine()
        {
            if( !string.IsNullOrEmpty( mLine ) )
            {
                GUILayout.Label( mLine, mSkin.label );
                mLine = string.Empty;
            }
        }
    }
}