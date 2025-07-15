import Header1 from "./Components/Header/Header1";
import Header2 from "./Components/Header/Header2";
import Cart from "./Components/Main/Cart";
import Home from "./Components/Main/Home";
import MyCalendar from "./Components/Main/MyCalender";
import MyEvents from "./Components/Main/MyEvents";
import SignUp from "./Components/Main/SignUp";
import Login from "./Components/Main/Login";
import AboutUs from "./Components/Main/AboutUs";

import { CssBaseline, ThemeProvider } from "@mui/material";
import { ColorModeContext, useMode } from "./theme";
import { Routes, Route } from "react-router-dom";
function App() {
  const [theme, colorMode] = useMode();
  return (
    <ColorModeContext.Provider value={colorMode}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Header1 />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/Cart" element={<Cart />} />
          <Route path="/Calender" element={<MyCalendar />} />
          <Route path="/MyEvents" element={<MyEvents />} />
          <Route path="/SignUp" element={<SignUp />} />
          <Route path="/Login" element={<Login />} />
          <Route path="/AboutUs" element={<AboutUs />} />
        </Routes>
      </ThemeProvider>
    </ColorModeContext.Provider>
  );
}

export default App;
