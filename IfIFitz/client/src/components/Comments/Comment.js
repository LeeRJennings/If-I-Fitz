import { Card, CardBody, CardSubtitle, CardText } from "reactstrap";
import { useNavigate } from "react-router-dom";

export const Comment = ({ comment }) => {
    return (
        <Card color="light" style={{width: '30%'}}>
            <CardBody>
                <CardText>
                    {comment.content}
                </CardText>
                <CardSubtitle className="mb-2 text-muted" tag="h6">
                    Posted by: {comment.userProfile.name} <br/> On: {comment.createdDateTime}
                </CardSubtitle>
            </CardBody>
        </Card>
    )
}