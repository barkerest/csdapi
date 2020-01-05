# Comodo/ITarian Service Desk API

The idea of this library was to make a simple to use interface for C# apps to talk to the
Comodo/ITarian Service Desk application.  But the API is incomplete.  For instance, you can
create or update customers, but not list them.  The naming conventions are inconsistent between
services and in at least one case the service name is completely different from the documented
value.  Testing sometimes just yields an error 500, so no way to know what you did wrong.  For
instance, the "updateuser" service just returns a 500 error.  Trying "updateUser" doesn't work,
trying "userId" instead of "id" doesn't work.  Trying "address" instead of "email" doesn't work.

Sure, I could probably reach out to support and eventually get pointed at the correct format and
endpoint, but truthfully, I'm tired of running into the inconsistencies.  If the documentation
was correct, then I would code according to the documentation happily, but I've found too many
errors in just the first half dozen services.

So, I'm leaving this project here for others to learn from.  Maybe Comodo/ITarian will update the
documentation, or at least come up with a consistent feel.  Maybe they could update the API
to return more useful information on failure, rather than just an "Internal Server Error"
message.  There are lots of maybes.  Unfortunately, this pretty much sealed the "maybe I can use
it for" question I first started this project with.

