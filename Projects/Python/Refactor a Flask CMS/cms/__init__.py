from flask import Flask, render_template, abort
from datetime import datetime
from flask_sqlalchemy import SQLAlchemy

## Application Configuration
app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///{}/{}'.format(app.root_path, 'content.db')
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
app.config['SECRET_KEY'] = 'b2de7FkqvkMyqzNFzxCkgnPKIGP6i4Rc'
#!

## Models
db = SQLAlchemy(app)

class Type(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(50), nullable=False)

class Content(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    title = db.Column(db.String(100), nullable=False)
    slug = db.Column(db.String(100), nullable=False)
    type_id = db.Column(db.Integer, db.ForeignKey('type.id'), nullable=False)
    type = db.relationship('Type', backref=db.backref('Content', lazy=True))
    body = db.Column(db.Text, nullable=False)
    created_at = db.Column(db.DateTime, nullable=False, default=datetime.utcnow())
    updated_at = db.Column(db.DateTime, nullable=False, default=datetime.utcnow())

class Setting(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    key = db.Column(db.String(100), nullable=False)
    value = db.Column(db.String(100), nullable=False)

class User(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    username = db.Column(db.String(100), unique=True, nullable=False)
    email = db.Column(db.String(100), unique=True, nullable=False)
    firstname = db.Column(db.String(100), unique=True, nullable=False)
    lastname = db.Column(db.String(100), unique=True, nullable=False)
#!

## Admin Routes
def requested_type(type):
    types = [row.name for row in Type.query.all()]
    return True if type in types else False

@app.route('/admin/', defaults={'type': 'page'})
@app.route('/admin/<type>')
def content(type):
    if requested_type(type):
        content = Content.query.join(Type).filter(Type.name == type)
        return render_template('admin/content.html', type=type, content=content)
    else:
        abort(404)

@app.route('/admin/create/<type>')
def create(type):
    if requested_type(type):
        types = Type.query.all()
        return render_template('admin/content_form.html', title='Create', types=types, type_name=type)
    else:
        abort(404)

@app.route('/admin/users')
def users():
    users = User.query.all()
    return render_template('admin/users.html', title='Users', users=users)

@app.route('/admin/settings')
def settings():
    settings = Setting.query.all()
    return render_template('admin/settings.html', title='Settings', settings=settings)
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
    titles = Content.query.with_entities(Content.slug, Content.title).join(Type).filter(Type.name == 'page')
    content = Content.query.filter(Content.slug == slug).first_or_404()
    return render_template('index.html', titles=titles, content=content)
#!

if __name__ == "__main__":
    app.run(debug=True)