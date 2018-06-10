import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import './ConfigEditor.css';
import { TextEditor } from './TextEditor';

interface ConfigEditorState {
    text: string;
    parsers: SentenceParserInfo[];
    errors: ParserError[];
    loading: boolean;
}

export class ConfigEditor extends React.Component<RouteComponentProps<{}>, ConfigEditorState> {
    constructor() {
        super();
        this.state = {
            text: '',
            parsers: [],
            errors: [],
            loading: true,
        };

        fetch('api/Configuration/Info')
            .then(response => response.json() as Promise<RawParserInfo[]>)
            .then(data => {
                var parsers = data.map(raw => {
                    return {
                        name: raw.name,
                        group: raw.group,
                        expression: new RegExp(raw.expression, 'i'),
                        examples: raw.examples,
                    } as SentenceParserInfo;
                });

                this.setState({ parsers: parsers, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : [
                this.renderInput(),
                this.renderErrors(),
                this.renderHelp(),
            ];

        return (
            <div className="editor">
                <div className="editor__intro">
                    <h1>Configuration editor</h1>
                    <p>This component lets you configure data using natural language.</p>
                    <input className="editor__submit" type="button" onClick={() => this.submit()} value="Submit" />
                </div>
                {contents}
            </div>
        );
    }

    private renderInput() {
        return (
            <TextEditor
                key="text"
                className="editor__input"
                highlights={this.state.errors}
                text={this.state.text}
                onchange={text => this.setState({ text: text })}
            />
        );
    }

    private renderErrors() {
        const errors = this.state.errors.length === 0
            ? <li>No errors reported</li>
            : this.state.errors.map((err, index) => (
                <li key={index}>
                    {err.message}
                </li>
            ));

        return (
            <ul key="error" className="editor__errors">
                {errors}
            </ul>
        );
    }

    private renderHelp() {
        return (
            <div key="help" className="editor__helpWrapper">
                <table className="table editor__help">
                    <thead>
                        <tr>
                            <th>Action</th>
                            <th>Group</th>
                            <th>Examples</th>
                        </tr>
                    </thead>
                    <tbody>
                    {this.state.parsers.map((parser, index) =>
                        <tr key={ index }>
                            <td>{ parser.name }</td>
                            <td>{parser.group}</td>
                            <td><ul>{parser.examples.map((ex, exIndex) => <li key={exIndex}>{ex}.</li>)}</ul></td>
                        </tr>
                    )}
                    </tbody>
                </table>
            </div>
        );
    }

    private submit() {
        fetch('api/Configuration/Parse', {
            method: 'POST',
            body: JSON.stringify({ text: this.state.text }),
            headers: {
                'Content-Type': 'application/json'
            },
        })
            .then(response => response.json() as Promise<ParserError[]>)
            .then(data => {
                this.setState({ errors: data });
            });
    }
}

interface RawParserInfo {
    name: string;
    group: string | null;
    expression: string;
    examples: string[];
}


interface SentenceParserInfo {
    name: string;
    group: string | null;
    expression: RegExp;
    examples: string[];
}

interface ParserError {
    message: string;
    startIndex: number;
    length: number;
}