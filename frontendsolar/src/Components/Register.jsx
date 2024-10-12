export default function Register ()
{
    return (
        <div className="Register">
            <div className="Register-form">
                <form>
                    <div className="UsernameInput">
                        <input placeholder="Username"/>
                    </div>
                    <div className="EmailInput">
                        <input type="email" placeholder="Email"/>
                    </div>
                    <div className="passwordInput">
                        <input type="password" placeholder="Password"/>
                    </div>

                    <div className="Button">
                        <button>Register</button>
                    </div>

                </form>
            </div>
        </div>
    )
}