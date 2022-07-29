import { Switch, Route, Redirect } from "react-router-dom";
import { NotFoundPage } from "../pages/notfound/NotFoundPage";
import { PermissionsPage } from "../pages/permissions/PermissionsPage";


const AppRouter = () => {
  return (    
      <Switch >
        <Route path="/permission" component={PermissionsPage}/>
        <Route path="/404" component={NotFoundPage} />
        <Route path="*">
          <Redirect to="/404" />
        </Route>
      </Switch>    
  );
};

export default AppRouter;
