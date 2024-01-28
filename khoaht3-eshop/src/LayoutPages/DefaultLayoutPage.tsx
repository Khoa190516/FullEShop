import NavigationBar from "../Components/NavigationBar/NavigationBar";

interface WithChildrenProps<T = React.ReactNode> {
    children: T;
}

const DefaultLayoutPage : React.FC<WithChildrenProps> = ({ children }) => {
    return (
        <div style={{width:"100%"}}>
            <div><NavigationBar/></div>
            <div style={{marginTop:"5%"}}>{children}</div>
        </div>
    );
}

export default DefaultLayoutPage;