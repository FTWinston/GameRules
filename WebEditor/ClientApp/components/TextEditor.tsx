import * as React from 'react';
import './TextEditor.css';

interface TextSegment {
    startIndex: number;
    length: number;
}

interface TextEditorProps {
    text: string;
    onchange: (text: string) => void;
    highlights: TextSegment[];
    className?: string;
}

export class TextEditor extends React.PureComponent<TextEditorProps, {}> {
    private isIE: boolean;
    private isWinPhone: boolean;
    private isIOS: boolean;

    private backdrop: HTMLDivElement;
    private highlights: HTMLDivElement;

    constructor(props: TextEditorProps) {
        super(props);

        var ua = window.navigator.userAgent.toLowerCase();
        this.isIE = !!ua.match(/msie|trident\/7|edge/);
        this.isWinPhone = ua.indexOf('windows phone') !== -1;
        this.isIOS = !this.isWinPhone && !!ua.match(/ipad|iphone|ipod/);
    }

    componentDidUpdate(prevProps: TextEditorProps, prevState: {}) {
        if (prevProps.highlights !== this.props.highlights) {
            this.renderHighlights(this.props.text);
        }
    }

    public render() {
        let classes = 'textEditor';
        if (this.props.className !== undefined) {
            classes = `${classes} ${this.props.className}`;
        }

        // iOS adds 3px of (unremovable) padding to the left and right of a textarea, so adjust highlights div to match
        const highlightStyle = this.isIOS ? {
            paddingLeft: '+=3px',
            paddingRight: '+=3px',
        } : {}

        return (
            <div className={classes}>
                <div className="textEditor__backdrop" ref={el => { if (el !== null) { this.backdrop = el; } }}>
                    <div className="textEditor__highlights" style={highlightStyle} ref={el => { if (el !== null) { this.highlights = el; } }} />
                </div>
                <textarea
                    className="textEditor__text"
                    value={this.props.text}
                    onChange={e => this.handleInput(e.target as HTMLTextAreaElement)}
                    onScroll={e => this.handleScroll(e.target as HTMLTextAreaElement)}
                />
            </div>
        );
    }

    private handleInput(textarea: HTMLTextAreaElement) {
        this.props.onchange(textarea.value);

        this.renderHighlights(textarea.value);
    }

    private handleScroll(textarea: HTMLTextAreaElement) {
        this.backdrop.scrollTop = textarea.scrollTop;
        this.backdrop.scrollLeft = textarea.scrollLeft;
    }

    private renderHighlights(text: string) {
        var highlightedText = this.addMarks(text);
        this.highlights.innerHTML = highlightedText;
    }

    private addMarks(text: string) {
        let highlights = this.props.highlights.sort((a, b) => a.startIndex < b.startIndex ? -1 : a.startIndex === b.startIndex ? 0 : 1);

        let offset = 0;
        for (const highlight of highlights) {
            const startPos = highlight.startIndex + offset;
            const endPos = startPos + highlight.length;

            text = text.slice(0, startPos) + '<mark>'
                + text.slice(startPos, endPos) + '</mark>'
                + text.slice(endPos);

            offset += 13;
        }

        text = text.replace(/\n$/g, '\n\n');

        if (this.isIE) {
            // IE wraps whitespace differently in a div vs textarea, this fixes it
            text = text.replace(/ /g, ' <wbr>');
        }

        return text;
    }
}
