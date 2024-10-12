export default function Login ()
{
    
    const fetchLogin = async () => {
        const response = await fetch('/api/login', {
            method: 'POST',
            body: JSON.stringify({username, password}),
            headers: {'Content-Type': 'application/json'},
        });
        const data = await response.json();
        if (data.token) {
            sessionStorage.setItem('authToken', data.token);
        }
    }
    
    
    return (
        <div className="Login">
            <div className="Login-form">
                <form>
                    <div className= "EmailInput">
                        <input type="email" placeholder= "Email"/>
                    </div>
                        <div className="passwordInput">
                        <input type="password" placeholder="Password"  />
                        </div>
                    
                    <div className="Button">
                        <button>Login</button>
                    </div>
                    
                </form>
            </div>
        </div>
    )
    
}