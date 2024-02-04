
import logo from './images/red-logo.png';
const Logo = () => {
    return (<>
        <a href="/">
            <img className='regularLogo' src={logo} style={{ "width": "10rem","textAlign":"left"}} />
        </a>
    </>);
}

export default Logo;