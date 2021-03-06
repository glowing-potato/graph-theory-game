import React from "react";
import { Route, Link } from "react-router-dom";
import "./Game.css";

class NextLink extends React.Component {
    render() {
        if (this.props.match.params.number >= this.props.levels) {
            return (
                <Link to="/levels">Next Group</Link>
            );
        } else {
            return (
                <Link to={`/${this.props.match.params.type}/${parseInt(this.props.match.params.number) + 1}`}>Next Puzzle</Link>
            );
        }
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
                to={"/" + window.location.href.split("/")[window.location.href.split("/").length - 2]}>&#9664; Back</Link>
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
                                        <a className="replay-button" onClick={this.reload}>Replay</a>
                                    </li>
                                    <li>
                                        <Route path="/:type/:number" render={(props) => <NextLink onClick={this.reload} levels={this.props.levels} {...props} />} />
                                    </li>
                                </ul>
                            </main>
                            <div className="padding" />
                        </div>
                        <div className="padding" />
                    </div>
                )}
            </div>
        );
    }
}
