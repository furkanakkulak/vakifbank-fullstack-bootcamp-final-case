export class User {
  expireDate: string | null = null;
  token: string | null = null;
  email: string | null = null;
  username: string | null = null;

  constructor(data: Partial<User> = {}) {
    Object.assign(this, data);
  }
}
