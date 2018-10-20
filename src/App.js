import React from "react";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import MainMenu from "./menus/MainMenu";
import LevelMenu from "./menus/LevelMenu";
import EulerCycle from "./games/EulerCycle";
import HamiltonianCycle from "./games/HamiltonianCycle";

export default class App extends React.Component {
    render() {
        return (
            <BrowserRouter>
                <Switch>
                    <Route path="/euler/:level" component={EulerCycle} />
                    <Route path="/hamiltonian/:level" component={HamiltonianCycle} />
                    <Route path="/:type" component={LevelMenu} />
                    <Route path="/" component={MainMenu} />
                </Switch>
            </BrowserRouter>
        );
    }
}
