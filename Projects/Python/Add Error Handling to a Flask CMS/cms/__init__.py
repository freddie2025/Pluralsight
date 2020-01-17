## Imports
from flask import Flask, render_template, abort, request
from cms.admin.models import Content, Type, User, Setting, db
from cms.admin import admin_bp
#!

## Application Configuration
app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///{}/{}'.format(app.root_path, 'content.db')
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
app.config['SECRET_KEY'] = 'b2de7FkqvkMyqzNFzxCkgnPKIGP6i4Rc'

from cms import handlers
#!

## Models
db.init_app(app)
#!

## Admin Routes
app.register_blueprint(admin_bp)
#!

## Front-end Route
@app.template_filter('pluralize')
def pluralize(string, end=None, rep=''):
    if end and string.endswith(end):
        return string[:-1 * len(end)] + rep
    else:
        return string + 's'

@app.route('/', defaults={'slug': 'home'})
@app.route('/<slug>')
def index(slug):
    content = Content.query.filter(Content.slug == slug).first_or_404()
    return render_template('index.html', content=content)

if __name__ == '__main__':
    app.run()
#!