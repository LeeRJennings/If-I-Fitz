import { Button, Card, CardBody, CardSubtitle, CardText } from "reactstrap";
import { useNavigate } from "react-router-dom";

export const Comment = ({ comment, user }) => {
    const navigate = useNavigate()

    return (
        <Card color="light" style={{width: '30%'}}>
            <CardBody>
                <CardSubtitle className="mb-2 text-muted" tag="h6">
                    Posted by: {comment.userProfile.name} <br/> On: {comment.createdDateTime}
                </CardSubtitle>
                <CardText>
                    {comment.content}
                </CardText>
                {comment.userProfileId === user.id ? 
                    <Button size="sm" color ="primary" onClick={() => navigate(`/posts/editComment/${comment.id}`)}>Edit</Button>
                    : ""
                }
            </CardBody>
        </Card>
    )
}