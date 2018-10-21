import React from "react";
import { BrowserRouter, Route, Link } from "react-router-dom";
import "./Game.css";

class NextLink extends React.Component {
    render() {
        return (
            <Link onClick={this.props.onClick} to={`/${this.props.match.params.type}/${parseInt(this.props.match.params.number) + 1}`}>Next Puzzle</Link>
        );
    }
}

export default class Game extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            "nonce": 0
        };
        this.reload = this.reload.bind(this);
    }

    reload() {
        this.setState({
            "nonce": this.state.nonce + 1
        }, () => {
            if (this.props.onReload) {
                this.props.onReload(this.state.nonce);
            }
        });
    }

    back() {
        console.log();
    }

    render() {
        return (
            <div className="game">
            <Link className="back-button"
                to={"/" + window.location.href.split("/")[window.location.href.split("/").length - 2]}>
                {"<- Back"}</Link>
                {this.props.children}
                {this.props.won && (
                    <div className="winning">
                        <div className="padding" />
                        <div className="row">
                            <div className="padding" />
                            <main>
                                <h2>You beat this level!</h2>
                                <ul>
                                    <li>
                                        <Link to="/">Home</Link>
                                    </li>
                                    <li>
                                        <a onClick={this.reload}>Replay</a>
                                    </li>
                                    <li>
                                        <BrowserRouter>
                                            <Route path="/:type/:number" render={(props) => <NextLink onClick={this.reload} {...props} />} />
                                        </BrowserRouter>
                                    </li>
                                </ul>
                            </main>
                            <div className="padding" />
                        </div>
                        <div className="padding" />
                    </div>
                )}
                <div className="clear-button" onClick={this.reload}>{"Clear"}</div>
            </div>
        );
    }
}
