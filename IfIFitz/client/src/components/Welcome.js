import { Card, CardBody, CardTitle, CardSubtitle } from "reactstrap"

export const Welcome = () => {
    return (
        <div style={{display: "flex", justifyContent: "center"}}>        
            <Card
            style={{
                position: "fixed",
                width: '20rem',
                margin: "0 50%",
                marginTop: "5%",
                textAlign: "center",
                border: "none"
            }}
            >
                <CardBody>
                    <CardTitle tag="h3">
                        Welcome to:
                    </CardTitle>
                    <CardTitle tag="h1">
                        If I Fitz
                    </CardTitle>
                    <CardSubtitle
                        className="mb-2 text-muted"
                        tag="h6"
                    >
                        An app for cats to share boxes of all shapes and sizes
                    </CardSubtitle>
                </CardBody>
                <img
                alt="If I Fitz logo"
                src="./images/If-I-Fitz-logo.png"
                width="100%"
                />
            </Card>
        </div>
    )
}